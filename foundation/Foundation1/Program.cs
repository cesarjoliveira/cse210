using System;
using System.Collections.Generic;

namespace FoundationProgram1
{
    
    public class Comment
    {
        private string _commenterName;
        private string _commentText;

        
        public Comment(string commenterName, string commentText)
        {
            _commenterName = commenterName;
            _commentText = commentText;
        }

    
        public string GetCommentDetails()
        {
            return $"{_commenterName}: {_commentText}";
        }
    }

    public class Video
    {
        private string _title;
        private string _author;
        private int _length; 
        private List<Comment> _comments;

        public Video(string title, string author, int length)
        {
            _title = title;
            _author = author;
            _length = length;
            _comments = new List<Comment>();
        }

        public void AddComment(Comment comment)
        {
            _comments.Add(comment);
        }

        public int GetNumberOfComments()
        {
            return _comments.Count;
        }

        public string GetVideoDetails()
        {
            return $"Title: {_title}\nAuthor: {_author}\nLength: {_length} seconds\nNumber of Comments: {GetNumberOfComments()}";
        }

        public List<string> GetComments()
        {
            List<string> commentDetails = new List<string>();
            foreach (var comment in _comments)
            {
                commentDetails.Add(comment.GetCommentDetails());
            }
            return commentDetails;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            
            List<Video> videos = new List<Video>();


            Video video1 = new Video("Learning C#", "John Doe", 300);
            video1.AddComment(new Comment("Alice", "Great video!"));
            video1.AddComment(new Comment("Bob", "Very informative."));
            video1.AddComment(new Comment("Charlie", "I learned a lot."));

            Video video2 = new Video("C# Advanced Topics", "Jane Smith", 450);
            video2.AddComment(new Comment("David", "This was helpful."));
            video2.AddComment(new Comment("Eve", "Excellent explanations."));
            video2.AddComment(new Comment("Frank", "I appreciate the depth of this tutorial."));

            Video video3 = new Video("C# for Beginners", "Mike Johnson", 600);
            video3.AddComment(new Comment("Grace", "Just what I needed!"));
            video3.AddComment(new Comment("Hank", "Clear and concise."));
            video3.AddComment(new Comment("Ivy", "Looking forward to more videos."));

            videos.Add(video1);
            videos.Add(video2);
            videos.Add(video3);

            foreach (var video in videos)
            {
                Console.WriteLine(video.GetVideoDetails());
                foreach (var comment in video.GetComments())
                {
                    Console.WriteLine(comment);
                }
                Console.WriteLine();
            }
        }
    }
}