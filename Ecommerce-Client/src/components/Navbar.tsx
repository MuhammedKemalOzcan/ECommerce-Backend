import { NavLink } from "react-router-dom";

function Navbar() {
  const listItems = [
    {
      id: 1,
      item: "HOME",
      path: "/",
    },
    {
      id: 2,
      item: "HEADPHONES",
      path: "/headphones",
    },
    {
      id: 3,
      item: "SPEAKERS",
      path: "/speakers",
    },
    {
      id: 4,
      item: "EARPHONES",
      path: "/earphones",
    },
  ];

  return (
    <div className="w-full h-[97px] bg-[#141414] flex flex-col justify-center items-center relative">
      <div className="flex w-[80%] justify-between text-white">
        <h1>Audiophile</h1>
        <nav className="flex justify-around w-[50%] ">
          {listItems.map((item) => (
            <NavLink
              key={item.id}
              className="hover:text-[#D87D4A]"
              to={item.path}
            >
              <p>{item.item}</p>
            </NavLink>
          ))}
        </nav>
      </div>
      <div className="border border-[#FFFFFF] opacity-25 w-[80%] absolute bottom-0 "></div>
    </div>
  );
}

export default Navbar;
