using System;
using System.Collections.Generic;
using System.Text;

namespace HaoDouCookBook.Models
{
    public class TopicModel
    {
        public string TopicPreviewImageSource { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string PreviewContent { get; set; }

        public string CreateTimeDescription { get; set; }

        public TopicModel()
        {
            TopicPreviewImageSource = string.Empty;
            Title = string.Empty;
            Author = string.Empty;
            PreviewContent = string.Empty;
            CreateTimeDescription = string.Empty;
        }
    }
}
