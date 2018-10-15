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
        private readonly CurrentWordPosition _currentWordPosition;

        [ImportingConstructor]
        public MyMouseHoverProcessorProvider(
            IVsEditorAdaptersFactoryService editorAdapters, CurrentWordPosition currentWordPosition)
        {
            _editorAdapters = editorAdapters;
            _currentWordPosition = currentWordPosition;
        }


        public IMouseProcessor GetAssociatedProcessor(IWpfTextView wpfTextView)
        {
            return new MyMouseHoverProcessor(wpfTextView, _editorAdapters, _currentWordPosition);
        }
    }


    internal class MyMouseHoverProcessor : MouseProcessorBase
    {
        private readonly IWpfTextView _view;
        private readonly CurrentWordPosition _currentWordPosition;
        private readonly IVsTextView _viewAdapter;

        public MyMouseHoverProcessor(IWpfTextView wpfTextView, IVsEditorAdaptersFactoryService editorAdapters,
            CurrentWordPosition currentWordPosition)
        {
            _view = wpfTextView;
            _currentWordPosition = currentWordPosition;
            _viewAdapter = editorAdapters.GetViewAdapter(wpfTextView);
        }

        public override void PostprocessMouseMove(MouseEventArgs e)
        {
            try
            {
                var mousePos = e.GetPosition(_view.VisualElement);

                int streamPosition = MouseHelper.GetMousePositionInTextView(_viewAdapter, _view, mousePos);

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
                _currentWordPosition.InvokeWordUnderMouseChanged(token.Span.Start, token.Span.End);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex}");
            }
        }
    }
}


