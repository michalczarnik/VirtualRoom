window.RoomRequestor = {
    addRoom: addRoom
}
var scope, http;
function addRoom(name, description, capacity) {
    console.log('adding room ' + name + ' ' + description + ' ' + capacity);
    requestData = {
        "adminid": "6e0ee133-012c-4800-b7d2-68ea93ef15e0",
        "name": name,
        "description": description,
        "capacity": capacity,
        "users": [
            "6e0ee133-012c-4800-b7d2-68ea93ef15e1",
            "6e0ee133-012c-4800-b7d2-68ea93ef15e5",
            "6e0ee133-012c-4800-b7d2-68ea93ef15e4",
            "6e0ee133-012c-4800-b7d2-68ea93ef15e3",
            "6e0ee133-012c-4800-b7d2-68ea93ef15e2"
        ],
        "inonuri": "uritoicon"
    };
    http.post('/api/roomapi/addroom', requestData).then(function mySuccess(response) {
        console.log('success')
        console.log(response.data);
        $('#addModal').modal('hide');
    }).catch(function myError(response) {
        console.log('error')
        console.log(response);
    });
};

var app = angular.module('VirtualRoom', []);
app.controller('VirtualRoomController', function ($scope, $http) {
    console.log('Controller intialized')
    $scope.addRoom = addRoom;
    scope = $scope;
    http = $http;
});