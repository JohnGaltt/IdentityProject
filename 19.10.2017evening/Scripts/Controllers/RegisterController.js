var RegisterController = function ($scope, $window, RegisterFactory) {
    $scope.registerForm = {
        userName: '',
        emailAddress: '',
        userLastName: '',
        password: '',
        confirmPassword: '',
        age: ''
    };

    $scope.register = function (isValid) {
        if (isValid) {
            alert("good job");
            var result = RegisterFactory($scope.registerForm.userName, $scope.registerForm.emailAddress, $scope.registerForm.userLastName, $scope.registerForm.password, $scope.registerForm.confirmPassword, $scope.registerForm.age);
        }
        else { alert("Something wrong"); }
       
    }
}

RegisterController.$inject = ['$scope', '$window', 'RegisterFactory'];