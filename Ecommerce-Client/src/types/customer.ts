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
  addresses?: Adress[];
}

export interface Adress {
  id: string;
  title: string;
  location: Location;
  isPrimary: boolean;
}

export interface AddAdress {
  title: string;
  location: Location;
  isPrimary: boolean;
}

export interface Location {
  street: string;
  city: string;
  country: string;
  zipCode: string;
}
