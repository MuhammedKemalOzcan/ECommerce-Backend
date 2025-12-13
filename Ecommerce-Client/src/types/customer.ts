export interface AddCustomer {
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
}
export interface Customer {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  address?: Adress[];
}

export interface AddAdress {
  street: string;
  city: string;
  country: string;
  zipCode: string;
}

export interface Adress {
  id: string;
  street: string;
  city: string;
  country: string;
  zipCode: string;
}
