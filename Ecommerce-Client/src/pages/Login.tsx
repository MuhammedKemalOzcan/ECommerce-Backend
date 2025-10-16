import { useNavigate } from "react-router-dom";
import facebook from "../assets/facebook.svg";
export default function Login() {
  const navigate = useNavigate();

  return (
    <div className="w-screen h-screen bg-[#F1F1F1] flex justify-center items-center text-white">
      <form className="bg-[#101010] w-[50%] h-[70%] p-8 rounded-[20px] flex flex-col gap-4 ">
        <p className="text-[30px]">Giriş Yap</p>
        <p>Hesabınıza giriş yapmak için e-posta ve şifrenizi giriniz</p>
        <label className="flex flex-col gap-2">
          E-posta
          <input
            placeholder="ornek@gmail.com"
            className="h-14 px-4 rounded-[8px] bg-black "
          />
        </label>
        <label className="flex flex-col">
          Şifre
          <input
            type="password"
            placeholder="****"
            className="h-14 px-4 rounded-[8px] bg-black"
          />
        </label>
        <button onClick={() => navigate("/")} className="bg-[#d87d4a] h-[48px] rounded-[8px] ">
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
      </form>
    </div>
  );
}
