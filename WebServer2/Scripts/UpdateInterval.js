$(function (busNumber) {
    setInterval(function () { $('#BusView').load("home/GetBusView/"+busNumber); }, 1000); // update evry sec
});