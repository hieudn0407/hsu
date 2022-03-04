"use trict"
var connection = new signalR.HubConnectionBuilder().withUrl("/KenoHub").build();
var connectionAccount = new signalR.HubConnectionBuilder().withUrl("/AccountHub").build();
var connectionPot = new signalR.HubConnectionBuilder().withUrl("/PotHub").build();
var connectionNews = new signalR.HubConnectionBuilder().withUrl("/NewsHub").build();

var ALERT_TITLE = "Thông Báo";
var ALERT_BUTTON_TEXT = "OK";

var slideIndex = 1;

function disabledEvent(e) {
    //if (e.stopPropagation) {
    //    e.stopPropagation();
    //} else if (window.event) {
    //    window.event.cancelBubble = true;
    //}
    //e.preventDefault();
    //return false;
}

document.addEventListener("contextmenu", function (e) {
    e.preventDefault();
}, false);

document.addEventListener("keydown", function (e) {
    if (e.ctrlKey && e.shiftKey && e.keyCode == 67) {
        disabledEvent(e);
    }
    if (e.ctrlKey && e.shiftKey && e.keyCode == 73) {
        disabledEvent(e);
    }
    if (e.ctrlKey && e.shiftKey && e.keyCode == 74) {
        disabledEvent(e);
    }
    if (e.keyCode == 83 && (navigator.platform.match("Mac") ? e.metaKey : e.ctrlKey)) {
        disabledEvent(e);
    }
    if (e.ctrlKey && e.keyCode == 85) {
        disabledEvent(e);
    }
    if (event.keyCode == 123) {
        disabledEvent(e);
    }
}, false);

function plusDivs(n) {
    showDivs(slideIndex += n);
}

function currentDiv(n) {
    showDivs(slideIndex = n);
}

function showDivs(n) {
    var i;
    var x = document.getElementsByClassName("slide-item");
    var dots = document.getElementsByClassName("slide-badge");
    if (n > x.length) { slideIndex = 1 }
    if (n < 1) { slideIndex = x.length }
    for (i = 0; i < x.length; i++) {
        x[i].classList.add("display-none");
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" slide-badge-active", "");
    }
    x[slideIndex - 1].classList.remove("display-none");
    dots[slideIndex - 1].className += " slide-badge-active";
}

setInterval(function () {
    plusDivs(1);
}, 5000);

var global_token = {
    value: getCookie("USER"),
    key: getCookie("KEY"),
    iv: getCookie("IV"),
};

if (document.getElementById) {
    window.alert = function (txt) {
        createCustomAlert(txt);
    }
}

function setCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + 1800000);
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + ";";
}

function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function eraseCookie(name) {
    document.cookie = name + '=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;';
}

function remove_cookie() {
    eraseCookie("USER");
    eraseCookie("KEY");
    eraseCookie("IV");
}

function sign_out() {
    remove_cookie();
    window.location.href = "/";
}

function update_token(token) {
    setCookie("USER", token.value, 0.021);
    setCookie("KEY", token.key, 0.021);
    setCookie("IV", token.iv, 0.021);
}

function createCustomAlert(txt) {
    d = document;

    if (d.getElementById("modalContainer")) return;

    mObj = d.getElementsByTagName("body")[0].appendChild(d.createElement("div"));
    mObj.id = "modalContainer";
    mObj.style.height = d.documentElement.scrollHeight + "px";

    alertObj = mObj.appendChild(d.createElement("div"));
    alertObj.id = "alertBox";
    if (d.all && !window.opera) alertObj.style.top = document.documentElement.scrollTop + "px";
    alertObj.style.left = (d.documentElement.scrollWidth - alertObj.offsetWidth) / 2 + "px";
    alertObj.style.visiblity = "visible";

    h1 = alertObj.appendChild(d.createElement("h1"));
    h1.appendChild(d.createTextNode(ALERT_TITLE));

    msg = alertObj.appendChild(d.createElement("p"));
    //msg.style = "color: red;";
    msg.innerHTML = txt;

    btn = alertObj.appendChild(d.createElement("a"));
    btn.id = "closeBtn";
    btn.appendChild(d.createTextNode(ALERT_BUTTON_TEXT));
    btn.focus();
    btn.onclick = function () { removeCustomAlert(); return false; }

    alertObj.style.display = "block";

}

function removeCustomAlert() {
    document.getElementsByTagName("body")[0].removeChild(document.getElementById("modalContainer"));
}

function Captcha() {
    var alpha = new Array('A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9');
    var i;
    for (i = 0; i < 6; i++) {
        var a = alpha[Math.floor(Math.random() * alpha.length)];
        var b = alpha[Math.floor(Math.random() * alpha.length)];
        var c = alpha[Math.floor(Math.random() * alpha.length)];
        var d = alpha[Math.floor(Math.random() * alpha.length)];
        var e = alpha[Math.floor(Math.random() * alpha.length)];
        var f = alpha[Math.floor(Math.random() * alpha.length)];
        var g = alpha[Math.floor(Math.random() * alpha.length)];
    }
    var code = a + ' ' + b + ' ' + ' ' + c + ' ' + d + ' ' + e + ' ' + f + ' ' + g;
    document.getElementById("mainCaptcha").innerHTML = code
    document.getElementById("mainCaptcha").value = code
}

function ValidCaptcha() {
    var string1 = removeSpaces(document.getElementById('mainCaptcha').value);
    var string2 = removeSpaces(document.getElementById('txtInput').value);
    if (string1 == string2) {
        return true;
    } else {
        return false;
    }
}

function removeSpaces(string) {
    return string.split(' ').join('');
}

function validation_phone(phone) {
    var regex = /(84|0[3|5|7|8|9])+([0-9]{8})\b/g;

    if (phone.match(regex))
        return true;

    return false;
}

$(function () {
    $('.lazy').Lazy({
        scrollDirection: 'vertical',
        effect: 'fadeIn',
        visibleOnly: true,
        onError: function (element) {
            console.log('error loading ' + element.data('src'));
        }
    });
});

var app = angular.module("STU", []).run();