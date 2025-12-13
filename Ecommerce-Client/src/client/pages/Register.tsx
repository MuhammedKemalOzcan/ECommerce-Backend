import { useForm, type SubmitHandler } from "react-hook-form";
import type { Register } from "../../types/User";
import FormField from "../../admin/components/products/FormField";
import { USER_VALIDATION_RULES } from "../../schemes/RegisterSchema";
import { useRegister } from "../../auth/UseRegister";
import { useNavigate } from "react-router-dom";
import FacebookIcon from "@mui/icons-material/Facebook";
import GoogleIcon from "@mui/icons-material/Google";

export default function Register() {
  const { register, handleSubmit } = useForm<Register>({
    defaultValues: {
      name: "",
      lastName: "",
      email: "",
      password: "",
      phoneNumber: "",
    },
  });
  const { mutate, loading } = useRegister();
  const navigate = useNavigate();

  const handleFormSubmit: SubmitHandler<Register> = async (data) => {
    await mutate(data);
    if (!loading) {
      navigate("/login");
    }
  };

  return (
    <form
      onSubmit={handleSubmit(handleFormSubmit)}
      className="flex items-center justify-center  w-full h-screen bg-[#F1F1F1]"
    >
      <div className="flex flex-col bg-[#101010] w-[30%] h-auto text-white gap-4 p-8 rounded-lg">
        <div className="flex flex-col gap-3">
          <h1>Welcome To Audiophile!</h1>
          <h2>Please Register</h2>
        </div>
        <FormField
          id="name"
          label="First Name"
          placeHolder="Your Name"
          type="text"
          {...register("name", USER_VALIDATION_RULES.name)}
        />
        <FormField
          id="surname"
          label="Last Name"
          placeHolder="Your Last Name"
          type="text"
          {...register("lastName", USER_VALIDATION_RULES.surname)}
        />
        <FormField
          id="email"
          label="Email"
          placeHolder="email@mail.com"
          type="email"
          {...register("email", USER_VALIDATION_RULES.email)}
        />
        <FormField
          id="password"
          label="Password"
          placeHolder="password"
          type="password"
          {...register("password", USER_VALIDATION_RULES.password)}
        />
        <FormField
          id="phoneNumber"
          label="Phone Number"
          placeHolder="0xxx xxx xx xx"
          type="text"
          {...register("phoneNumber", USER_VALIDATION_RULES.phoneNumber)}
        />
        <div className="flex flex-col gap-6">
          <button className="btn-1 p-2" type="submit">
            Register
          </button>
          <div className="flex items-center justify-between gap-2 text-black">
            <button className="flex items-center gap-2 h-12 bg-white p-3">
              <FacebookIcon color="primary"  />
              <p>Facebook ile giriş yap</p>
            </button>
            <button className="flex items-center h-12 gap-2 bg-orange-800 p-3">
              <GoogleIcon/>
              <p>Google ile giriş yap</p>
            </button>
          </div>
        </div>

        <div className="flex gap-3">
          <p>Hesabın var mı?</p>
          <button
            onClick={() => navigate("/login")}
            className="text-orange-500"
          >
            Giriş yap
          </button>
        </div>
      </div>
    </form>
  );
}
