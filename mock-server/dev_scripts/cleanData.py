import json
import argparse

def clean_order_data(orders):
    for order in orders:
        # Replace sensitive fields with generic data
        order['ClientName'] = f"Client {order['CustomerId']}"
        order['FirstName'] = f"FirstName {order['CustomerId']}"
        order['LastName'] = f"LastName {order['CustomerId']}"
        order['Email'] = "example@example.com"
        order['Street'] = f"Street {order['CustomerId']}"
        order['City'] = f"City {order['CustomerId']}"
        order['Appartment'] = f"Apt {order['CustomerId']}"

        # Update fields in AccountingProviderDto if present
        if 'AccountingProvider' in order and order['AccountingProvider']:
            order['AccountingProvider']['DocumentUrl'] = "https://example.com"

        # Replace phone number if present
        if 'Phone' in order:
            order['Phone'] = "+972501234567"

    return orders

def main():
    parser = argparse.ArgumentParser(description='Clean sensitive data from OrderDto JSON.')
    parser.add_argument('-i', '--input', required=True, help='Input JSON file with orders')
    parser.add_argument('-o', '--output', default='clean.json', help='Output JSON file for cleaned data')
    
    args = parser.parse_args()

    with open(args.input, 'r', encoding='utf-8') as file:
        orders = json.load(file)

    cleaned_orders = clean_order_data(orders)

    with open(args.output, 'w', encoding='utf-8') as file:
        json.dump(cleaned_orders, file, indent=4, ensure_ascii=False)

if __name__ == "__main__":
    main()
