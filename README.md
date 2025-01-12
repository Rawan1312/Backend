# E-commerce Application API

This repository contains ASP.NET Core application with RESTful API endpoints for e-commerce application. The API allows you to interact with products in the store.

`This is a teamwork assignment where you will work as a team within your group`

## How to work

1. One team member (admin) should fork the repo and add other members to that admin repo as collaborators.
2. The other team members should fork then clone the forked repo (the admin repo).
3. Any change/update made should be submitted to admin repo as pull request.
4. Each change should be done in a separate pull request.
5. Pull request must be reviewed by all members before merged to admin repo.
6. Admin should open a PR to the original (Integrify) repo.

Please ask your instructor or supporting instructor if you have any questions or need help.

## Level 1: Basic Requirements

In this level, the application includes the following features:

1. Identify Entities: Identify the main entities that need to be stored in the database. These could include customers, products, categories, orders, etc.
2. Define Attributes: For each entity, list and define the attributes or properties associated with it. For example, for a "customer" entity, attributes might include "id," "firstName," "lastName," "email" and so on.
3. Establish Relationships: Determine the relationships between entities. Relationships can be one-to-one, one-to-many, or many-to-many. For instance, in an E-commerce system, a "customer" may have multiple "orders".
4. Key: When establishing relationships, remember to create a key in your ERD to explain the notation used for relationships.
5. According to the ERD above, create the entities, and build the database with Entity Framework Core.
6. Create basic CRUD operations for each endpoint.
7. Use authentication and role-based authorization

## Level 2: Additional Requirements

In addition to the basic requirements, the application enhances its functionality with the following features:

1. Include pagination functionality to the method getting all products.
2. Implement search functionality to allow users to search for specific products based on keywords or specific fields (e.g., by title).
3. Add validation checks to ensure the data meets certain criteria before executing the actions.

## Level 3: Advanced Requirements

If you have a higher skill level and finish the previous requirements before the deadline, you can tackle the following bonus tasks:

1. Refactor method getting all books or products to also handle query parameters for filtering and sorting products based on specific criteria (e.g., price range, by title, by date, etc). Pagination still need to be integrated.
2. Use claim-based or resource-based where applicable.
3. Peer Review:
   - Review 2 assignments from other teams.
   - Provide constructive feedback and suggestions for improvement.

`Please note that the bonus requirements and reviews are optional and can be completed if you have additional time and advanced skills.`

Happy coding!
#   B a c k e n d 
 
 #   b a c k e n d 
 
 

GET https://sda-3-onsite-backend-teamwork-bw5k.onrender.com

###

GET https://sda-3-onsite-backend-teamwork-bw5k.onrender.com/api/v1/users

###

POST https://sda-3-onsite-backend-teamwork-bw5k.onrender.com/api/v1/users
Content-Type: application/json

{
"Name":"mohamed moh",
"Email":"mohmoh@gmail.com",
"Password":"772255"
}

###

GET https://sda-3-onsite-backend-teamwork-bw5k.onrender.com/api/v1/users/2c6fc907-fde7-4c17-80ae-495fce652ff9

###

PUT http://localhost:5125/api/users/2c6fc907-fde7-4c17-80ae-495fce652ff9
Content-Type: application/json

{
"Name": "mona",
"Email": "mona@example.com",
"Password": "mona0099"
}

###

DELETE http://localhost:5125/api/v1/users/8a31ed78-d88d-4567-a36e-a15690cc434c

###

GET https://sda-3-onsite-backend-teamwork-bw5k.onrender.com/api/v1/category

###

POST https://sda-3-onsite-backend-teamwork-bw5k.onrender.com/api/v1/category
Content-Type: application/json

{
"CategoryName":"makup",
"Description":"makup product"
}

###

GET https://sda-3-onsite-backend-teamwork-bw5k.onrender.com/api/v1/category/9a272870-b241-4cf5-9cd7-5fc99c78d8fb

###

DELETE https://sda-3-onsite-backend-teamwork-bw5k.onrender.com/api/v1/category/91866267-7f5d-4ce0-88c0-33008f60bad0

###

PUT https://sda-3-onsite-backend-teamwork-bw5k.onrender.com/api/v1/category/9a272870-b241-4cf5-9cd7-5fc99c78d8fb
Content-Type: application/json

{
"CategoryName": "mashine",
"Description":"Devices"
}

###

GET https://sda-3-onsite-backend-teamwork-bw5k.onrender.com/api/v1/products

###

POST https://sda-3-onsite-backend-teamwork-bw5k.onrender.com/api/v1/products
Content-Type: application/json

{
"Name": "iphone 13 plus",
"Price": 200.80,
"CategoryId":"9a272870-b241-4cf5-9cd7-5fc99c78d8fb"
}

###

GET https://sda-3-onsite-backend-teamwork-bw5k.onrender.com/api/v1/products/3e6233aa-e685-445f-8f66-8c41ca68e6ff

###

PUT https://sda-3-onsite-backend-teamwork-bw5k.onrender.com/api/v1/products/08c3d37d-54bd-4da1-b2b9-fd89a7da3be1
Content-Type: application/json

{
"Name": "iphone 14",
"Price": 100.33
}

###

DELETE https://sda-3-onsite-backend-teamwork-bw5k.onrender.com/api/v1/products/3e6233aa-e685-445f-8f66-8c41ca68e6ff

###

GET https://sda-3-onsite-backend-teamwork-bw5k.onrender.com/api/v1/address

###

POST https://sda-3-onsite-backend-teamwork-bw5k.onrender.com/api/v1/address
Content-Type: application/json

{
"City" : "medinah",
"State" : "almaboth",
"UserId":"2bf2df50-4223-45c4-8840-9e5589eab10f"
}

###

GET https://sda-3-onsite-backend-teamwork-bw5k.onrender.com/api/v1/address/6091656a-52ec-4e3e-9115-6254de541c4e

###

PUT https://sda-3-onsite-backend-teamwork-bw5k.onrender.com/api/v1/address/6091656a-52ec-4e3e-9115-6254de541c4e
Content-Type: application/json

{
"City": "alula",
"State": "alazizyah"
}

###

DELETE https://sda-3-onsite-backend-teamwork-bw5k.onrender.com/api/v1/address/6091656a-52ec-4e3e-9115-6254de541c4e

###

# GET https://sda-3-onsite-backend-teamwork-bw5k.onrender.com/api/v1/auth

# Accept: application/json
<!-- 
###

POST https://sda-3-onsite-backend-teamwork-bw5k.onrender.com/api/v1/auth/register
Content-Type: application/json

{
"name": "mona8877",
"email": "mona8877@gmail.com",
"password": "004433"
}

###

POST https://sda-3-onsite-backend-teamwork-bw5k.onrender.com/api/v1/auth/login
Content-Type: application/json

{
"email": "mona8877@gmail.com",
"password": "004433"
}

###

GET https://sda-3-onsite-backend-teamwork-bw5k.onrender.com/api/v1/users/profile
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI0MjNmZjQ4Ni1jNDEzLTQ5MDYtYjBjNy1kMmMyOTkyMjMxZTkiLCJ1bmlxdWVfbmFtZSI6ImFsYWE5OSIsInJvbGUiOiJBZG1pbiIsIm5iZiI6MTcyODI5Mzk0MywiZXhwIjoxNzI4Mjk0MjQzLCJpYXQiOjE3MjgyOTM5NDMsImlzcyI6IkxvY2FsaG9zdElzc3VlciIsImF1ZCI6IkxvY2FsaG9zdEF1ZGllbmNlIn0.nVPhZdQsCycG-E9L9Wm7ns_3U7to6ROxbSK0fBf6dRw

###

GET https://sda-3-onsite-backend-teamwork-bw5k.onrender.com/api/v1/orders

###

POST https://sda-3-onsite-backend-teamwork-bw5k.onrender.com/api/v1/orders
Content-Type: application/json

{
"NameOrder": "iphone 14",
"Price": 100.33,
"UserId":"7118b8a2-d497-47ba-85ae-6fd83dc4d73c"
}

###

GET https://sda-3-onsite-backend-teamwork-bw5k.onrender.com/api/v1/orders/6e2c0cb4-9ef0-42cd-97fa-2d5c1e3ac829

###

DELETE https://sda-3-onsite-backend-teamwork-bw5k.onrender.com/api/v1/orders/5208c418-8167-46b8-949e-5446e380136e -->
