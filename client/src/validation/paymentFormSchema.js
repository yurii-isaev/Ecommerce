import * as Yup from 'yup';
import { number, string } from 'yup';
import * as patterns from '@/validation/patterns';

const { cardHolderNamePattern, creditCardNumberPattern, expirationMonthPattern, ccvPattern } = patterns;

const paymentFormSchema = Yup.object().shape({

  nameOnCard: string()
      .required('Card holder name is required')
      .matches(cardHolderNamePattern, 'Card holder name is invalid'),

  numberOnCard: string()
      .required('Credit card number is required')
      .matches(creditCardNumberPattern, 'Credit card number is invalid'),

  expMonth: string()
      .required('Expiration month is required')
      .matches(expirationMonthPattern, 'Expiration month is invalid'),

  expYear: number()
      .required('Expiration year is required')
      .min(new Date().getFullYear(), 'Expiration year is invalid'),

  // CVV (Card Verification Value)
  cvv: string()
      .required('CVV is required')
      .matches(ccvPattern, 'CVV is invalid'),
});

export default paymentFormSchema;