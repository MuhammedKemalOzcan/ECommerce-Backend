import { useNavigate } from "react-router-dom";
import facebook from "../../assets/facebook.svg";
import { useForm, type SubmitHandler } from "react-hook-form";
import type { User } from "../../types/User";
import { useAuthStore } from "../../auth/authStore";
import { useLogin } from "../../auth/useLogin";
import { useEffect } from "react";
import { ArrowBigRight } from "lucide-react";

export default function Login() {
  const navigate = useNavigate();
  const { mutate } = useLogin();
  const { register, handleSubmit } = useForm<User>();
  const user = useAuthStore((s) => s.user);

  const onSubmit: SubmitHandler<User> = async (data) => {
    await mutate(data.email, data.password);
  };


  useEffect(() => {
    if (user) {
      navigate("/");
    }
  }, [user]);

  return (
    <div className="w-screen h-screen bg-[#F1F1F1] flex justify-center items-center text-white">
      <form
        onSubmit={handleSubmit(onSubmit)}
        className="bg-[#101010] w-auto h-auto p-8 rounded-[20px] flex flex-col gap-4 "
      >
        <p className="text-[30px]">Giriş Yap</p>
        <p>Hesabınıza giriş yapmak için e-posta ve şifrenizi giriniz</p>
        <label className="flex flex-col gap-2">
          E-posta
          <input
            {...register("email", { required: "email required!" })}
            placeholder="ornek@gmail.com"
            className="h-14 px-4 rounded-[8px] bg-black "
          />
        </label>
        <label className="flex flex-col">
          Şifre
          <input
            {...register("password", {
              required: "password required!",
              minLength: {
                value: 6,
                message: "Password must contains at least 6 characters.",
              },
            })}
            type="password"
            placeholder="****"
            className="h-14 px-4 rounded-[8px] bg-black"
          />
        </label>
        <button type="submit" className="bg-[#d87d4a] h-[48px] rounded-[8px] ">
          Giriş Yap
        </button>
        <div className="border"></div>
        <div className="flex gap-3 items-center justify-between">
          <div className="flex items-center w-[60%] justify-center gap-3 bg-gray-700 w-auto h-14 rounded-[8px] px-4">
            <img src={facebook} />
            <p>Google ile devam et</p>
          </div>
          <div className="flex items-center w-[60%] justify-center gap-3 bg-gray-700 w-auto h-14 rounded-[8px] px-4">
            <img src={facebook} />
            <p>Facebook ile devam et</p>
          </div>
        </div>
        <p>
          Hesabın yok mu?{" "}
          <button
            onClick={() => navigate("/register")}
            className="text-orange-800"
          >
            Kayıt ol
          </button>
        </p>
        <button onClick={() => navigate("/")} className="flex gap-3">
          Kayıt olmadan devam et
          <ArrowBigRight />
        </button>
      </form>
    </div>
  );
}
