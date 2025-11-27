import headphones from "../../../assets/Headphones.svg";
import speakers from "../../../assets/Speaker.svg";
import earphones from "../../../assets/earphones.svg";

export default function MenuItem() {
  const menuItems = [
    { name: "HEADPHONES", image: headphones },
    { name: "SPEAKERS", image: speakers },
    { name: "EARPHONES", image: earphones },
  ];

  return (
    <div className="flex ">
      {menuItems.map((item) => (
        <div className="flex justify-around items-center gap-20">
          <h1>{item.name}</h1>
        </div>
      ))}
    </div>
  );
}
