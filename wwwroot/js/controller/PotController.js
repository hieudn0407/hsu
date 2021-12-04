app.controller('PotController', ['$scope', '$timeout', '$http', function ($scope, $timeout, $http) {
    var element = document.getElementsByClassName("roll-body");
    var roll = 0;
    var stop = 0;
    var coin_after = 0;
    var my_timer = 0;
    var pot_star = 0;
    var auto_roll = 0;
    var coefficient = {
        coe_1: 0.5,
        coe_2: 1.5,
        coe_3: 5,
        coe_4: 17.5,
        coe_5: 60,
        coe_6: 200,
        coe_7: 500
    };
    $scope.win_coin = {
        coin_2: {
            star_1: 2 * coefficient.coe_1,
            star_2: 2 * coefficient.coe_2,
            star_3: 2 * coefficient.coe_3,
            star_4: 2 * coefficient.coe_4,
            star_5: 2 * coefficient.coe_5,
            star_6: 2 * coefficient.coe_6,
            star_7: 2 * coefficient.coe_7
        },
        coin_10: {
            star_1: 10 * coefficient.coe_1,
            star_2: 10 * coefficient.coe_2,
            star_3: 10 * coefficient.coe_3,
            star_4: 10 * coefficient.coe_4,
            star_5: 10 * coefficient.coe_5,
            star_6: 10 * coefficient.coe_6,
            star_7: 10 * coefficient.coe_7
        },
        coin_50: {
            star_1: 50 * coefficient.coe_1,
            star_2: 50 * coefficient.coe_2,
            star_3: 50 * coefficient.coe_3,
            star_4: 50 * coefficient.coe_4,
            star_5: 50 * coefficient.coe_5,
            star_6: 50 * coefficient.coe_6,
            star_7: 50 * coefficient.coe_7
        },
        coin_100: {
            star_1: 100 * coefficient.coe_1,
            star_2: 100 * coefficient.coe_2,
            star_3: 100 * coefficient.coe_3,
            star_4: 100 * coefficient.coe_4,
            star_5: 100 * coefficient.coe_5,
            star_6: 100 * coefficient.coe_6,
            star_7: 100 * coefficient.coe_7
        },
    };

   

    $scope.current_coin_coefficient = $scope.win_coin.coin_2;
    $scope.pot_loading = false;
    $scope.pot_bet_coin = 2;
    $scope.is_auto_roll = false;

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
            var user_coin_mobile = document.getElementById("user_coin_mobile");
            var user_star = document.getElementById("rra" + pot_star);
            user_coin.innerHTML = "Xu hiện có: " + coin_after;
            user_coin_mobile.innerHTML = "Xu hiện có: " + coin_after;
            user_star.classList.add("roll-reward-active");
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
            var element = document.getElementsByClassName("rra");
            for (var i = 0; i < element.length; i++) {
                element[i].classList.remove("roll-reward-active");
            }

            $scope.pot_loading = true;
            connectionPot.invoke("PotRoll", global_token, $scope.pot_bet_coin).catch(function (err) {
                return console.error(err.toString());
            });
        }
    }

    $scope.Set_Bet = (coin) => {
        if ($scope.pot_loading == false) {
            var element = document.getElementsByClassName("roll-center-bet");
            var element_mobile = document.getElementsByClassName("rcb-mobile");
            var element_rra = document.getElementsByClassName("rra");

            for (var i = 0; i < element_rra.length; i++) {
                element_rra[i].classList.remove("roll-reward-active");
            }

            for (var i = 0; i < element.length; i++) {
                element[i].classList.remove("roll-center-bet-active");
            }

            for (var i = 0; i < element_mobile.length; i++) {
                element_mobile[i].classList.remove("roll-center-bet-active");
            }

            var rcb = document.getElementsByClassName("rcb-" + coin);
            rcb[0].classList.add("roll-center-bet-active");

            var rcb_mobile = document.getElementsByClassName("rcb-mobile-" + coin);
            rcb_mobile[0].classList.add("roll-center-bet-active");

            $scope.pot_bet_coin = coin;
            if (coin == 2) {
                $scope.current_coin_coefficient = $scope.win_coin.coin_2;
            }
            else if (coin == 10) {
                $scope.current_coin_coefficient = $scope.win_coin.coin_10;
            }
            else if (coin == 50) {
                $scope.current_coin_coefficient = $scope.win_coin.coin_50;
            }
            else if (coin == 100) {
                $scope.current_coin_coefficient = $scope.win_coin.coin_100;
            }
        }
    };

    $scope.goAuto = () => {
        $scope.is_auto_roll = true;
        auto_roll = setInterval(function () {
            $scope.goQuay();
        }, 1000);
    };

    $scope.goStop = () => {
        $scope.is_auto_roll = false;
        clearInterval(auto_roll);
    };

    connectionPot.on("ReceiveRollPot", function (cookie_token, cell, coin, star) {
        if (cookie_token == null) {
            remove_cookie();
            window.location.reload();
            return;
        }

        if (cell == -1) {
            update_token(cookie_token);
            clearInterval(auto_roll);
            $scope.pot_loading = false;
            $scope.is_auto_roll = false;
            $scope.$apply();
            alert("Không đủ <b>Xu</b> để quay.");
            return;
        }

        update_token(cookie_token);
        stop = -1;
        roll = 0;

        my_timer = setInterval(quay, 50);
        setTimeout(function () { clearInterval(my_timer); my_timer = setInterval(quay, 100); }, 3000);
        setTimeout(function () { clearInterval(my_timer); my_timer = setInterval(quay, 150); }, 5000);
        setTimeout(function () { clearInterval(my_timer); my_timer = setInterval(quay, 200); }, 7000);
        setTimeout(function () { stop = cell; coin_after = coin; pot_star = star; clearInterval(my_timer); my_timer = setInterval(quay, 250); }, 9000);
    });
}]);