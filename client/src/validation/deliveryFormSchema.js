import * as Yup from 'yup';
import { string } from 'yup';
import * as patterns from '@/validation/patterns';

const { emailPattern, fullNamePattern, addressPattern, cityPattern, statePattern, zipCodePattern } = patterns;

const deliveryFormSchema = Yup.object().shape({
  
  fullName: string()
      .required('Full name is required')
      .matches(fullNamePattern, 'Full name is invalid'),
  
  email: string()
      .required('Email is required')
      .email('Email is invalid')
      .matches(emailPattern, 'Email is invalid'),

  address: string()
      .required('Address is required')
      .matches(addressPattern, 'Address is invalid'),
  
  city: string()
      .required('City is required')
      .matches(cityPattern, 'Сity is invalid'),
  
  state: string()
      .required('State is required')
      .matches(statePattern, 'State is invalid'),
  
  zipCode: string()
      .required('Zip code is required')
      .matches(zipCodePattern, 'Zip code is invalid'),
});

export default deliveryFormSchema;