import intro from "../../../assets/intro.svg";

export default function StoreIntro() {
  return (
    <div className="flex flex-col p-6 gap-6 lg:flex-row lg:gap-60 md:items-center">
      <img
        src={intro}
        alt="Audio Gear"
        className="w-full h-auto rounded-lg md:max-w-[400px] lg:max-w-[540px] object-cover"
      />
      <div className="flex flex-col items-center justify-center text-center gap-4 lg:w-[30%] lg:text-start">
        <h1 className="w-[80%] text-[30px] lg:w-full">
          BRINGING YOU THE <span className="text-orange-500">BEST</span> AUDIO
          GEAR
        </h1>
        <p className="text-[15px] text-gray-400">
          Located at the heart of New York City, Audiophile is the premier store
          for high end headphones, earphones, speakers, and audio accessories.
          We have a large showroom and luxury demonstration rooms available for
          you to browse and experience a wide range of our products. Stop by our
          store to meet some of the fantastic people who make Audiophile the
          best place to buy your portable audio equipment.
        </p>
      </div>
    </div>
  );
}
