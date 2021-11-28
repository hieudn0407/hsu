app.controller('HomeController', ['$scope', '$timeout', '$http', function ($scope, $timeout, $http) {
    $scope.PlayNowModal = false;
    $scope.KenoModal = false;
    $scope.ChanLeModal = false;
    $scope.loading = true;
    $scope.selectChanLe = 0;
    $scope.text = "QUAY SỐ BẮT ĐẦU SAU";

    if (connection.q == "Disconnected") {
        connection.start().then(function () {
            connection.invoke("GetKeno").catch(function (err) {
                return console.error(err.toString());
            });
        }).catch(function (err) {
            return console.error(err.toString());
        });
    }

    var clearActive = () => {
        var element = document.getElementsByClassName("chanle-select-span");
        for (var i = 0; i < element.length; i++) {
            element[i].classList.remove("modal-chanle-active");
        }
    };

    $scope.ChanLeSelected = (num) => {
        clearActive();

        if (num == 1) {
            var element = document.getElementsByClassName("modal-chanle-le");
            element[0].classList.add("modal-chanle-active");
        }
        else {
            var element = document.getElementsByClassName("modal-chanle-chan");
            element[0].classList.add("modal-chanle-active");
        }

        $scope.selectChanLe = num;
    };

    

    $scope.PlayNow = () => {
        $scope.PlayNowModal = true;
    };

    $scope.ClosePlayNow = () => {
        $scope.PlayNowModal = false;
    };

    $scope.goBetNumber = () => {
        $scope.PlayNowModal = false;
        $scope.KenoModal = true;
    };

    $scope.goBetChanLe = () => {
        $scope.PlayNowModal = false;
        $scope.ChanLeModal = true;
    };

    $scope.CloseKeno = () => {
        $scope.KenoModal = false;
    };

    $scope.CloseChanLe = () => {
        $scope.ChanLeModal = false;
    };

    $scope.goAcceptKeno = () => {
        var number = $scope.Keno_number;
        var coin = $scope.Keno_Coin;

        if (number == undefined || number == null) {
            alert("Vui lòng chọn số muốn cược.");
            return;
        }

        if (coin == undefined || coin == null) {
            alert("Vui lòng chọn số <b>Xu</b> muốn cược.");
            return;
        }

        if (coin == 0) {
            alert("Vui lòng đặt <b>Xu</b> lớn hơn 0.");
            return;
        }

        connection.invoke("KenoBet", $scope.KenoCurrent.id, number, coin).catch(function (err) {
            return console.error(err.toString());
        });
    };

    $scope.goAcceptChanLe = () => {
        var number = $scope.selectChanLe;
        var coin = $scope.ChanLe_Coin;

        if (number == 0) {
            alert("Vui lòng chọn chẵn hoặc lẻ.");
            return;
        }

        if (coin == undefined || coin == null) {
            alert("Vui lòng chọn số <b>Xu</b> muốn cược.");
            return;
        }

        if (coin == 0) {
            alert("Vui lòng đặt <b>Xu</b> lớn hơn 0.");
            return;
        }

        connection.invoke("ChanLeBet", $scope.ChanLeCurrent.id, number, coin).catch(function (err) {
            return console.error(err.toString());
        });
    };

    connection.on("ReceiveKeno", function (record, chanle, historyKeno, historyChanLe) {
        $scope.KenoCurrent = record.data[0];
        $scope.KenoLast = record.data[1];

        $scope.ChanLeCurrent = chanle.data[0];
        $scope.ChanLeLast = chanle.data[1];

        $scope.datauser = record.data[1].users;
        $scope.datauserchanle = chanle.data[1].users;

        $scope.historyKeno = historyKeno;
        $scope.historyChanLe = historyChanLe;

        $scope.text = "QUAY SỐ BẮT ĐẦU SAU";
        if (record.data[0].result != -1 && record.data[0].updatedusercoins) {
            $scope.text = "ĐỢI QUAY SỐ LẦN TỚI";
        }

        var countdown = record.data[0].countdown;
        $scope.minus = (countdown - countdown % 60) / 60;
        $scope.seconds = countdown % 60;

        var tick = setInterval(function () {
            $scope.seconds = $scope.seconds - 1;

            if ($scope.minus == 0 && $scope.seconds == -1) {
                clearInterval(tick);
                countdown = 0;
                connection.invoke("GetKeno").catch(function (err) {
                    return console.error(err.toString());
                });

                return;
            }

            if ($scope.seconds == -1) {
                $scope.seconds = 59;
                $scope.minus = $scope.minus - 1;
            }

            $scope.$apply()
        }, 1000);

        $scope.loading = false;
    });

    connection.on("ReceiveKenoBet", function (response) {
        alert(response.message);

        if (response.code == 200) {
            setTimeout(function () {
                window.location.reload();
            }, 2000);
        }
    });

    connection.on("ReceiveChanLeBet", function (response) {
        alert(response.message);

        if (response.code == 200) {
            setTimeout(function () {
                window.location.reload();
            }, 2000);
        }
    });
}]);