using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Utilities;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Formatting;

namespace AddMenuItem4
{
    [Export(typeof(IMouseProcessorProvider))]
    [Name("MyMouseHoverProcessorProvider")]
    [ContentType("code")]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    [Order(Before = "VisualStudioMouseProcessor")]
    internal sealed class MyMouseHoverProcessorProvider : IMouseProcessorProvider//, IStartable
    {
        private readonly IVsEditorAdaptersFactoryService _editorAdapters;

        [ImportingConstructor]
        public MyMouseHoverProcessorProvider(
            IVsEditorAdaptersFactoryService editorAdapters)
        {
            _editorAdapters = editorAdapters;
        }


        public IMouseProcessor GetAssociatedProcessor(IWpfTextView wpfTextView)
        {
            return new MyMouseHoverProcessor(wpfTextView, _editorAdapters);
        }
    }


    internal class MyMouseHoverProcessor : MouseProcessorBase
    {
        private readonly IWpfTextView _view;
        private readonly IVsTextView _viewAdapter;

        //private readonly Subject<MousePosData> _mouseMovmentSubject = new Subject<MousePosData>();

        public MyMouseHoverProcessor(IWpfTextView wpfTextView, IVsEditorAdaptersFactoryService editorAdapters)
        {
            _view = wpfTextView;
            _viewAdapter = editorAdapters.GetViewAdapter(wpfTextView);

            //_mouseMovmentSubject
            //    .Sample(TimeSpan.FromMilliseconds(30))
            //    .ObserveOnDispatcher()
            //    .Subscribe(OnMouseMove);
        }



        public override void PostprocessMouseMove(MouseEventArgs e)
        {
            try
            {
                var mousePos = e.GetPosition(_view.VisualElement);

                int streamPosition = MouseHelper.GetMousePositionInTextView(_viewAdapter, _view, mousePos);
                //_viewAdapter.GetNearestPosition(line, column, out int position1, out int spaces);

                var document = _view.TextSnapshot.TextBuffer.GetRelatedDocuments().First();
                var syntaxTree = document.GetSyntaxTreeAsync().GetAwaiter().GetResult();
                var token = syntaxTree.GetRoot().FindToken(streamPosition);

                if (streamPosition != 0)
                {
                    Debug.WriteLine($"position = {streamPosition} token={token.ToString()}");
                }
                else
                {
                    Debug.WriteLine($"No token under mouse");
                }

                base.PreprocessMouseMove(e);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex}");
            }
        }
    }
}

//private void OnMouseMove(MousePosData mouseMove)
//{
//    if (!_active)
//        return;

//    int position = mouseMove.SyntaxTree.GetPosition(mouseMove.DocPosition);
//    int startPositionForProjectionBuffer = _view.TextBuffer.GetStartPosition();
//    position += startPositionForProjectionBuffer;

//    ConfirmMouseIsEntirelyWithinLine(ref mouseMove, ref position);

//    _mouseHoverHandlers.ForEach(handler =>
//    {
//        try
//        {
//            handler.OnMouseMove(mouseMove, position, _view);
//        }
//        catch (Exception ex)
//        {
//            ErrorNotificationLogger.LogErrorWithoutShowingErrorNotificationUI("OnMouseMove", ex);
//        }
//    });
//}

///// <summary>
///// Sometimes we get mouse position even when cursor is not entirely in line limits, but rather a bit above or below.
///// This method makes sure mouse is near middle of line, and changes position to 0 if not.
///// </summary>
//private void ConfirmMouseIsEntirelyWithinLine(ref MousePosData mouseMove, ref int position)
//{
//    _viewAdapter.GetLineAndColumn(position, out int piLine, out int piColumn);

//    POINT[] ppt = new POINT[1];
//    _viewAdapter.GetPointOfLineColumn(piLine, piColumn, ppt);
//    _viewAdapter.GetLineHeight(out int piLineHeight);

//    var zoomLevel = _view.ZoomLevel / 100.0;
//    var mouseYNormalized = mouseMove.MousePosition.Y * zoomLevel;

//    double lowRange = ppt[0].y + (0.1 * piLineHeight);
//    double highRange = lowRange + (0.8 * piLineHeight);
//    if (!(mouseYNormalized > lowRange && mouseYNormalized < highRange))
//    {
//        position = 0;
//        mouseMove.DocPosition = new DebuggerPosition();
//    }
//}
