app.controller('PotController', ['$scope', '$timeout', '$http', function ($scope, $timeout, $http) {
    var element = document.getElementsByClassName("roll-body");
    var roll = 0;
    var stop = 0;
    var coin_after = 0;
    var my_timer = 0;
    $scope.pot_loading = false;
    $scope.pot_bet_coin = 2;

    if (connectionPot.q == "Disconnected") {
        connectionPot.start().then(function () {
        }).catch(function (err) {
            return console.error(err.toString());
        });
    }

    function quay() {
        for (var i = 0; i < element.length; i++) {
            element[i].classList.remove("active");
            element[i].classList.remove("active-result");
        }

        if (stop != -1 && roll == stop) {
            var result = document.getElementsByClassName("roll-" + roll);
            var user_coin = document.getElementById("user_coin");
            user_coin.innerHTML = "Xu hiện có: " + coin_after;
            result[0].classList.add("active-result");
            $scope.pot_loading = false;
            $scope.$apply();
            clearInterval(my_timer);
        }

        var ele = document.getElementsByClassName("roll-" + roll);
        ele[0].classList.add("active");
        roll++;

        if (roll == 20) {
            roll = 0;
        }
    }

    $scope.goQuay = () => {
        if ($scope.pot_loading == false) {
            $scope.pot_loading = true;
            connectionPot.invoke("PotRoll", global_token, $scope.pot_bet_coin).catch(function (err) {
                return console.error(err.toString());
            });
        }
    }

    $scope.Set_Bet = (coin) => {
        var element = document.getElementsByClassName("roll-center-bet");
        for (var i = 0; i < element.length; i++) {
            element[i].classList.remove("roll-center-bet-active");
        }
        var rcb = document.getElementsByClassName("rcb-" + coin);
        rcb[0].classList.add("roll-center-bet-active");
        $scope.pot_bet_coin = coin;
    };

    connectionPot.on("ReceiveRollPot", function (cookie_token, cell, coin) {
        if (cookie_token == null) {
            remove_cookie();
            window.location.reload();
            return;
        }

        update_token(cookie_token);
        stop = -1;
        roll = 0;

        my_timer = setInterval(quay, 50);
        setTimeout(function () { clearInterval(my_timer); my_timer = setInterval(quay, 100); }, 3000);
        setTimeout(function () { clearInterval(my_timer); my_timer = setInterval(quay, 150); }, 5000);
        setTimeout(function () { clearInterval(my_timer); my_timer = setInterval(quay, 200); }, 7000);
        setTimeout(function () { stop = cell; coin_after = coin; clearInterval(my_timer); my_timer = setInterval(quay, 250); }, 9000);
    });
}]);