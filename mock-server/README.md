# mock-server

This project simulates the ToyCRM services and webhook connection.

## Getting Started

Ensure you have Node.js installed. If not, download it from [Node.js](https://nodejs.org/).

1. Install project dependencies:

   `npm install`

2. Modify the webhook url 

3. Run the development server:

   `npm run dev`

4. Open `http://localhost:8008/` on your browser.

   Alternative - Sent a request to the following endpoints to simulate an action:
## API Routes
| Route              | HTTP Method | Description                                      |
|--------------------|-------------|--------------------------------------------------|
| `/orders`          | GET         | Get a list of orders. You can include a `count` query parameter to limit the number of orders returned. |
| `/orders`          | POST        | Create a new order and initiate a web hook call. |
| `/orders/:id`      | GET         | Get details of a specific order by ID.          |
| `/orders/:id`      | PUT         | Update a specific order by ID and initiate a web hook call. |
| `/orders/:id`      | DELETE      | Delete a specific order by ID and initiate a web hook call. |


## Testing

You can test the webhook connection using the test server:
```
python dev_scripts/test_webhook_server.py
```
<i>Make sure you have Flask installed by running</i>
```
pip install Flask
```
Then you can initiate a request to the node js server.  You should see a successfull message on the test server:
```
Success: Received a POST request on /webhook/orders
```
