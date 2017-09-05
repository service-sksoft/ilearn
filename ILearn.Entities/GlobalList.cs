namespace ILearn.Entities
    {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    public static class GlobalList
        {

        public static List<Topic> Topics = new List<Topic>();

        public static List<QuesList> Questions = new List<QuesList>();

        public static List<MultipleChoice> MultipleChoice = new List<MultipleChoice>();

        public static List<ExternalResource> ExternalResources = new List<ExternalResource>();

        public static List<TutorialTitle> TutorialTitle = new List<TutorialTitle>();

        public static List<TutorialSubtitle> TutorialSubtitle = new List<TutorialSubtitle>();

        public static List<TutorialContent> TutorialContent = new List<TutorialContent>();
        }
    }