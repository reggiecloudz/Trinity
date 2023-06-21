const connection = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

var _connectionId = '';

function notify(data) {
    const toastLiveExample = document.getElementById('liveToast')
    const toastBootstrap = bootstrap.Toast.getOrCreateInstance(toastLiveExample);
    const toastBody = document.getElementById('toastBody');
    toastBody.innerHTML = data;
    toastBootstrap.show()
}

connection.on('displayNotification',(data) => {
    notify(data);
});

connection.start()
    .then(function () {
        connection.invoke('getConnectionId')
            .then(function (connectionId) {
                _connectionId = connectionId
                console.log(_connectionId)
            })
    })
    .catch(function (error) {
        console.log(error);
        setTimeout(() => start(), 5000);
    })
