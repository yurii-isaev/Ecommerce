import * as Yup from 'yup';

const forgotPasswordSchema = Yup.object().shape({
  email: Yup.string()
      .required('Email is required')
      .email('Email is invalid'),
});

export default forgotPasswordSchema;