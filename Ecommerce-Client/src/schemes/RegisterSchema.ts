import type { RegisterOptions } from "react-hook-form";
import type { Register } from "../types/User";

export const USER_VALIDATION_RULES = {
  name: {
    required: "Kullanıcı adı zorunludur.",
    minLength: {
      value: 3,
      message: "Kullanıcı adı en az 3 karakter içermelidir.",
    },
    maxLength: {
      value: 30,
      message: "Kullanıcı Adı en fazla 30 karakter içerebilir.",
    },
  } satisfies RegisterOptions<Register, "name">,
  email: {
    required: "Email adresi zorunludur.",
  } satisfies RegisterOptions<Register, "email">,
  password: {
    required: "Şifre zorunludur",
    minLength: {
      value: 6,
      message: "Kullanıcı adı en az 6 karakter içermelidir.",
    },
    maxLength: {
      value: 20,
      message: "Şifre en fazla 20 karakter içerebilir.",
    },
  } satisfies RegisterOptions<Register, "password">,
  phoneNumber: {
    required: "Telefon numarası zorunludur.",
  } satisfies RegisterOptions<Register, "phoneNumber">,
  surname: {
    required: "Soyad zorunludur",
    minLength: {
      value: 3,
      message: "Soyadı en az 3 karakter içermelidir.",
    },
    maxLength: {
      value: 30,
      message: "SoyAdı en fazla 30 karakter içerebilir.",
    },
  } satisfies RegisterOptions<Register, "lastName">,
};
