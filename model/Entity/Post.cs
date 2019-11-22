namespace model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Post")]
    public partial class Post
    {
        [StringLength(500)]
        public string Id { get; set; }

        [StringLength(500)]
        public string ImageURL { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? Author_Id { get; set; }

        public int? Catagory_Id { get; set; }

        public string Content { get; set; }

        public int? IsBreakingNews { get; set; }

        public int? NumOfVisitors { get; set; }

        public virtual Catagory Catagory { get; set; }

        public virtual User User { get; set; }
    }
}
