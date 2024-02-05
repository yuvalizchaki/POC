import axios from 'axios';
import { API_BASE_URL } from '../config';
import { ScreenProfile } from '../types/screenProfile.types';

//********************************** screen **********************************//
export const pairScreen = (code: string, screenProfileId : number) => {
  return axios({
    method: 'post',
    url: API_BASE_URL+'/screens',
    data: {
      pairingCode: code,
      screenProfileId: screenProfileId
    }
  });
}
export const removeScreen = () => {

}
export const getScreen = () => {

}
export const getAllScreens = () => {

}
//********************************** screen profile **********************************//
export const createScreenProfile = (nameArg : string) => {
  return axios({
    method: 'post',
    url: API_BASE_URL+'/screen-profiles',
    data: {
      name: nameArg,
    }
  });
}
export const getScreenProfile = (id :number) => {

}
export const updateScreenProfile = () => {

}
export const deleteScreenProfile = (id :number) => {
  return axios({
    method: 'delete',
    url: API_BASE_URL+'/screen-profiles/'+id,
  });
}

export const getAllScreenProfiles = async (): Promise<ScreenProfile[]> => {
  try {
    const response = await axios.get(API_BASE_URL+'/screen-profiles');
    console.log('response:', response);
    return response.data;
  } catch (error) {
    console.error('Error fetching screen profiles:', error);
    return [];
  }
};
//********************************** orders **********************************//
export const getAllOrders = () => {

}
export const getOrderById = () => {

}
//********************************** webhook **********************************//
export const webhookOrderAdded = () => {

}
export const webhookOrderUpdated = () => {

}
export const WebhookOrderDeleted = () => {

}
  // TODO: Admin Implement other functions