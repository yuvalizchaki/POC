import { OrderStatus } from "../types/crmTypes.types";

export const getColor = (keyword: string) => {
    const colors: { [key: string]: string } = {
        Outgoing: '#d32f2f',
        Incoming: 'rgb(46, 125, 50)',
        Pending: 'rgb(115, 65, 147)',
        [`status_${OrderStatus.Completed}`]: 'rgb(2, 136, 209)',
        [`status_${OrderStatus.Canceled}`]: '#d32f2f',
        [`status_${OrderStatus.Approved}`]: 'rgb(46, 125, 50)',
        [`status_${OrderStatus.Draft}`]: 'rgb(237, 108, 2)',
        [`status_${OrderStatus.Returned}`]: 'rgb(0, 77, 114)',
        [`status_${OrderStatus.Ready}`]: '#734193',
    };
    return colors[keyword] || 'inherit'
};