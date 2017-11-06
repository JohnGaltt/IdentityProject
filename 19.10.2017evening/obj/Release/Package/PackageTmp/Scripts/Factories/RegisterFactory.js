var RegisterFactory = function ($http, $q, $location, $window) {
  
    return function (userName, emailAddress, userLastName, password, confirmPassword, age) {
 
       
        $http.post(
            '/Account/Register', {
                UserName: userName,
                Email: emailAddress,
                UserLastName: userLastName,
                Password: password,
                ConfirmPassword: confirmPassword,
                Age: age
            }
        ).then(function (res) {
            alert("hello");
            console.log("res.data.success");
            console.log(res.data);
            if (res.data.success) {
                alert("Your credentials are okay:)");
                window.location.pathname = 'Home/Confirm';               
            }
            else {
                alert("This username is already taken");            
                window.location.pathname = 'Account/Register';               
            }
        });   
           
            };
    }


RegisterFactory.$inject = ['$http', '$q', '$location', '$window'];