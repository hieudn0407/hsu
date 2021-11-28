app.controller('MenuController', ['$scope', '$timeout', '$http', function ($scope, $timeout, $http) {

    $scope.CloseMenu = () => {
        var menubody = document.getElementsByClassName("menu-body");
        menubody[0].style.width = "0px";
        menubody[0].style.left = "-100%";

        setTimeout(function () {
            var menu = document.getElementsByClassName("menu-overload");
            menu[0].setAttribute("hidden", "hidden");
        }, 500);
    };

    $scope.goAccount = () => {
        window.location.href = "/dang-nhap";
    };

    $scope.LoadMenu = () => {
        var menu = document.getElementsByClassName("menu-overload");
        menu[0].removeAttribute("hidden");

        setTimeout(function () {
            var menubody = document.getElementsByClassName("menu-body");
            menubody[0].style.width = "270px";
            menubody[0].style.left = "0px";
        }, 100);
    };

    connectionAccount.on("ReceiveNotification", function (response) {
        $scope.Notification = true;
        $scope.NotificationText = response.message;

        var marquee = document.getElementById("marquee");
        marquee.start();

        setTimeout(function () {
            $scope.Notification = false;
            $scope.$apply();
        }, 30000);

        $scope.$apply();
    });

}]);