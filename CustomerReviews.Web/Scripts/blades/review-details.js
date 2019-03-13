angular.module('CustomerReviews.Web')
    .controller('CustomerReviews.Web.CustomerReviewController',
        [
            '$scope', 'platformWebApp.bladeNavigationService', 'CustomerReviews.WebApi',
            'virtoCommerce.catalogModule.items',
            function($scope, bladeNavigationService, reviewsApi, items) {

                var blade = $scope.blade;
                var currentId = blade.currentEntityId;
                var productId = blade.currentEntity.productId;

                blade.isLoading = true;
                blade.headIcon = 'fa-comments';

                blade.refresh = function() {
                    return reviewsApi.GetCustomerReview({ id: currentId },
                        function(data) {
                            blade.currentEntityId = data.id;
                            blade.currentEntity = data;
                            blade.mode = $scope.blade.mode;
                        });
                };

                items.get({ id: productId },
                    function(data) {
                        $scope.product = data;
                        blade.subtitle = data.name;
                        blade.isLoading = false;
                    });

                blade.openProductBlade = function() {
                    var newBlade = {
                        id: "reviewItemDetail",
                        itemId: productId,
                        controller: 'virtoCommerce.catalogModule.itemDetailController',
                        template: 'Modules/$(VirtoCommerce.Catalog)/Scripts/blades/item-detail.tpl.html'
                    }

                    bladeNavigationService.showBlade(newBlade, blade);
                }
            }
        ]);
