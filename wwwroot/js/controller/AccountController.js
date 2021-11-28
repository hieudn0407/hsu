app.controller('AccountController', ['$scope', '$timeout', '$http', function ($scope, $timeout, $http) {
    $scope.SendNotification = () => {
        connectionAccount.invoke("SendNotification", $scope.NotificationTextSend).catch(function (err) {
            return console.error(err.toString());
        });
    };
}]);