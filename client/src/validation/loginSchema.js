import * as Yup from 'yup';

const loginSchema = Yup.object().shape({
  email: Yup.string()
      .required('Email is required')
      .email('Email is invalid'),

  password: Yup.string()
      .min(6, 'Password must be at least 6 characters')
      .required('Password is required'),
});

export default loginSchema;