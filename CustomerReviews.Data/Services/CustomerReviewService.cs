using System;
using System.Linq;
using CustomerReviews.Core.Model;
using CustomerReviews.Core.Services;
using CustomerReviews.Data.Model;
using CustomerReviews.Data.Repositories;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Data.Infrastructure;

namespace CustomerReviews.Data.Services
{
    public class CustomerReviewService : ServiceBase, ICustomerReviewService
    {
        private readonly Func<ICustomerReviewRepository> _repositoryFactory;

        public CustomerReviewService(Func<ICustomerReviewRepository> repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public CustomerReview[] GetByIds(string[] ids)
        {
            using (var repository = _repositoryFactory())
            {
                return repository.GetByIds(ids)
                    .Select(x => x.ToModel(AbstractTypeFactory<CustomerReview>.TryCreateInstance())).ToArray();
            }
        }

        public void SaveCustomerReviews(CustomerReview[] items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));


            var pkMap = new PrimaryKeyResolvingMap();
            using (var repository = _repositoryFactory())
            {
                using (var changeTracker = GetChangeTracker(repository))
                {
                    var alreadyExistEntities =
                        repository.GetByIds(items.Where(m => !m.IsTransient()).Select(x => x.Id).ToArray());
                    foreach (var derivativeContract in items)
                    {
                        var sourceEntity = AbstractTypeFactory<CustomerReviewEntity>.TryCreateInstance()
                            .FromModel(derivativeContract, pkMap);
                        var targetEntity = alreadyExistEntities.FirstOrDefault(x => x.Id == sourceEntity.Id);


                        if (targetEntity != null)
                        {
                            if (targetEntity.Rate > 5)
                                targetEntity.Rate = 5;

                            if (targetEntity.Rate < 0)
                                targetEntity.Rate = 0;

                            changeTracker.Attach(targetEntity);
                            sourceEntity.Patch(targetEntity);
                        }
                        else
                        {
                            if (sourceEntity.Rate > 5)
                                sourceEntity.Rate = 5;

                            if (sourceEntity.Rate < 0)
                                sourceEntity.Rate = 0;

                            repository.Add(sourceEntity);
                        }
                    }

                    CommitChanges(repository);
                    pkMap.ResolvePrimaryKeys();
                }
            }
        }

        public void DeleteCustomerReviews(string[] ids)
        {
            using (var repository = _repositoryFactory())
            {
                repository.DeleteCustomerReviews(ids);
                CommitChanges(repository);
            }
        }
    }
}
