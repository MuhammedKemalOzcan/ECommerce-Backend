import intro from "../../../assets/intro.svg";

export default function StoreIntro() {
  return (
    <div className="flex items-center justify-between gap-32">
      <div className="flex flex-col w-[30%] gap-4">
        <h1>
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
      <img src={intro} className="size-[50%]" />
    </div>
  );
}
