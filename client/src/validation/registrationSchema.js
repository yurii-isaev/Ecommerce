import * as Yup from 'yup';

const registrationSchema = Yup.object().shape({
  username: Yup.string().required('Usename is required'),

  email: Yup.string()
      .required('Email is required')
      .email('Email is invalid'),

  password: Yup.string()
      .min(6, 'Password must be at least 6 characters')
      .required('Password is required'),

  confirmPassword: Yup.string()
      .oneOf([Yup.ref('password'), null], 'Passwords must match')
      .required('Confirm Password is required'),

  acceptTerms: Yup.string().required('Accept Ts & Cs is required')
});

export default registrationSchema;