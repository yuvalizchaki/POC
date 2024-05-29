// TODO: remove

export const getColor = (keyword: string) => {
    const colors: { [key: string]: string } = {
        Outgoing: '#d32f2f',
        Incoming: 'rgb(46, 125, 50)',
        Pending: 'rgb(115, 65, 147)',
        Completed: 'rgb(2, 136, 209)',
        Cancelled: '#d32f2f',
        Approved: 'rgb(46, 125, 50)',
        Draft: 'rgb(237, 108, 2)',
    };

    return colors[keyword] || 'inherit'
};

export const ordersData = [
    {
        time: '10:00',
        id: 123,
        type: 'Incoming',
        status: 'Completed',
        client_name: 'John Doe',
        address: '123 Main St'
    },
    {
        time: '10:30',
        id: 124,
        type: 'Outgoing',
        status: 'Pending',
        client_name: 'Jane Smith',
        address: '456 Elm St'
    },
    {
        time: '11:00',
        id: 125,
        type: 'Incoming',
        status: 'Approved',
        client_name: 'Alice Johnson',
        address: '789 Maple St'
    },
    {
        time: '11:30',
        id: 126,
        type: 'Outgoing',
        status: 'Cancelled',
        client_name: 'Bob Brown',
        address: '101 Oak St'
    },
    {
        highlight: true,
        time: '12:00',
        id: 127,
        type: 'Incoming',
        status: 'Draft',
        client_name: 'Charlie Davis',
        address: '234 Pine St'
    },
    {
        time: '12:30',
        id: 128,
        type: 'Outgoing',
        status: 'Pending',
        client_name: 'Eve Adams',
        address: '567 Cedar St'
    },
    {
        time: '13:00',
        id: 129,
        type: 'Incoming',
        status: 'Completed',
        client_name: 'Frank White',
        address: '890 Birch St'
    },
    {
        time: '13:30',
        id: 130,
        type: 'Outgoing',
        status: 'Approved',
        client_name: 'Grace Black',
        address: '123 Spruce St'
    },
    {
        time: '14:00',
        id: 131,
        type: 'Incoming',
        status: 'Draft',
        client_name: 'Hank Green',
        address: '456 Fir St'
    },
    {
        time: '14:30',
        id: 132,
        type: 'Outgoing',
        status: 'Pending',
        client_name: 'Ivy Brown',
        address: '789 Willow St'
    }
];

export const inventoryData = [
    {
        id: 301,
        type: 'Outgoing',
        department: 'Furniture',
        productName: 'Folding Chair',
        amount: 50,
        isBundle: false,
        orders: 3
    },
    {
        id: 302,
        type: 'Incoming',
        department: 'Furniture',
        productName: 'Round Table',
        amount: 30,
        isBundle: true,
        orders: 2
    },
    {
        id: 303,
        type: 'Outgoing',
        department: 'Furniture',
        productName: 'Event Tent',
        amount: 20,
        isBundle: false,
        orders: 3
    },
    {
        id: 304,
        type: 'Incoming',
        department: 'Sound',
        productName: 'PA System',
        amount: 60,
        isBundle: true,
        orders: 1
    },
    {
        highlight: true,
        id: 305,
        type: 'Outgoing',
        department: 'Sound',
        productName: 'Microphone',
        amount: 10,
        isBundle: false,
        orders: 1
    },
    {
        id: 306,
        type: 'Incoming',
        department: 'Sound',
        productName: 'Projector',
        amount: 100,
        isBundle: true,
        orders: 1
    },
    {
        id: 307,
        type: 'Outgoing',
        department: 'Furniture',
        productName: 'Stage Platform',
        amount: 80,
        isBundle: false,
        orders: 2
    },
    {
        id: 308,
        type: 'Incoming',
        department: 'Silverware',
        productName: 'Decorative Lights',
        amount: 40,
        isBundle: true,
        orders: 3
    },
    {
        id: 309,
        type: 'Outgoing',
        department: 'Silverware',
        productName: 'Catering Set',
        amount: 25,
        isBundle: false,
        orders: 2
    },
    {
        id: 310,
        type: 'Incoming',
        department: 'Silverware',
        productName: 'Heater',
        amount: 70,
        isBundle: true,
        orders: 1
    }
];
