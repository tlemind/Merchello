/**
 * @ngdoc controller
 * @name Merchello.Marketing.Dialogs.OfferConstraintLineItemQuantityController
 * @function
 *
 * @description
 * The controller to configure the line item quantity component constraint
 */
angular.module('merchello').controller('Merchello.Marketing.Dialogs.OfferConstraintProductLineItemQuantityController',
    ['$scope',
    function($scope) {
        $scope.loaded = true;

        $scope.operator = 'gt';
        $scope.quantity = 0;

        // exposed methods
        $scope.save = save;

        function init() {
            loadExistingConfigurations()
        }

        function loadExistingConfigurations() {
            var operator = $scope.dialogData.getValue('operator');
            var quantity = $scope.dialogData.getValue('quantity');
            $scope.operator = operator === '' ? 'gt' : operator;
            $scope.quantity = quantity;
            loaded = true;
        }

        /**
         * @ngdoc method
         * @name save
         * @function
         *
         * @description
         * Saves the configuration
         */
        function save() {
            $scope.dialogData.setValue('quantity', $scope.quantity);
            $scope.dialogData.setValue('operator', $scope.operator);
            $scope.submit($scope.dialogData);
        }

        // Initialize the controller
        init();
    }]);
