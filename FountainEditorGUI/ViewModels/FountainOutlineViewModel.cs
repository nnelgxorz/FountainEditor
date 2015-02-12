using System.Collections.Generic;
using System.Collections.ObjectModel;
using FountainEditor.Messaging;
using FountainEditorGUI.Messages;

namespace FountainEditorGUI.ViewModels
{
    public sealed class FountainOutlineViewModel : ViewModelBase
    {
        private ICollection<string> elements;

        public ICollection<string> Elements {
            get { return elements; }
            set {
                if (elements != value) {
                    elements = value;

                    OnPropertyChanged();
                }
            }
        }

        public FountainOutlineViewModel(IMessagePublisher<DocumentMessage> documentMessagePublisher)
        {
            documentMessagePublisher.Subscribe(DocumentChanged);
        }

        private void DocumentChanged(DocumentMessage message)
        {
            // TODO: Assign to the 'Elements' property
            // 
        }
    }
}
