# web-security-academy.net-samples

The code in this repo can be used to help solve some labs from portswigger, there is a asp.net server which can be used to extract information like CSRF tokens.

The folder **PrivateServerWithCorsAllOrigins** has a server which:

1. Have a endpoint `/csrf` that receives QueryString parameters `url` and `cookie` and consumes the url with the cookie as parameter and extract from response a 
input[name=csrf] value and return as response.

If one of the parameters is null the server return a `Bad Request`, if the csrf doesn't exists a `Conflict` is returned.
