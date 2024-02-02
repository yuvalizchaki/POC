from flask import Flask, request
import json

app = Flask(__name__)

@app.route('/webhook/orders', methods=['POST'])
def orders_post():
    print("Success: Received a POST request on /webhook/orders")
    # Check if the request data is JSON
    if request.is_json:
        # Parse the JSON data
        data = request.get_json()
        print("Received data:", json.dumps(data, indent=4))
    else:
        # If not JSON, just print the raw data
        print("Received raw data:", request.data)
    return "POST request received", 200

@app.route('/webhook/orders/<order_id>', methods=['PUT', 'DELETE'])
def orders_put_delete(order_id):
    if request.method == 'PUT':
        print(f"Success: Received a PUT request on /webhook/orders/{order_id}")
    elif request.method == 'DELETE':
        print(f"Success: Received a DELETE request on /webhook/orders/{order_id}")
    return f"{request.method} request received for order {order_id}", 200

if __name__ == '__main__':
    app.run(port=5177, debug=True)
