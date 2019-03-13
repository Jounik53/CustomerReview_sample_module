using System;
using System.ComponentModel.DataAnnotations;
using CustomerReviews.Core.Model;
using VirtoCommerce.Platform.Core.Common;

namespace CustomerReviews.Data.Model
{
    public class CustomerReviewEntity : AuditableEntity
    {
        [StringLength(128)]
        public string AuthorNickname { get; set; }

        [Required]
        [StringLength(1024)]
        public string Content { get; set; }

        public bool IsActive { get; set; }

        [Required]
        [StringLength(128)]
        public string ProductId { get; set; }

        [Required]
        [StringLength(1024)]
        public string Virtues { get; set; }

        [Required]
        [StringLength(1024)]
        public string Disadvantages { get; set; }

        public int Rate { get; set; }

        public virtual CustomerReview ToModel(CustomerReview customerReview)
        {
            if (customerReview == null)
                throw new ArgumentNullException(nameof(customerReview));

            customerReview.Id = Id;
            customerReview.CreatedBy = CreatedBy;
            customerReview.CreatedDate = CreatedDate;
            customerReview.ModifiedBy = ModifiedBy;
            customerReview.ModifiedDate = ModifiedDate;

            customerReview.AuthorNickname = AuthorNickname;
            customerReview.Content = Content;
            customerReview.IsActive = IsActive;
            customerReview.ProductId = ProductId;

            customerReview.Rate = Rate;
            customerReview.Virtues = Virtues;
            customerReview.Disadvantages = Disadvantages;

            return customerReview;
        }

        public virtual CustomerReviewEntity FromModel(CustomerReview customerReview, PrimaryKeyResolvingMap pkMap)
        {
            if (customerReview == null)
                throw new ArgumentNullException(nameof(customerReview));

            pkMap.AddPair(customerReview, this);

            Id = customerReview.Id;
            CreatedBy = customerReview.CreatedBy;
            CreatedDate = customerReview.CreatedDate;
            ModifiedBy = customerReview.ModifiedBy;
            ModifiedDate = customerReview.ModifiedDate;

            AuthorNickname = customerReview.AuthorNickname;
            Content = customerReview.Content;
            IsActive = customerReview.IsActive;
            ProductId = customerReview.ProductId;

            Rate = customerReview.Rate;
            Virtues = customerReview.Virtues;
            Disadvantages = customerReview.Disadvantages;

            return this;
        }

        public virtual void Patch(CustomerReviewEntity target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            target.AuthorNickname = AuthorNickname;
            target.Content = Content;
            target.IsActive = IsActive;
            target.ProductId = ProductId;

            target.Rate = Rate;
            target.Virtues = Virtues;
            target.Disadvantages = Disadvantages;
        }
    }
}
