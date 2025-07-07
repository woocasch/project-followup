import './sidebar.scss';
import close from '@assets/sidebar/close-menu.svg';
import open from '@assets/sidebar/open-menu.svg';
import { useMemo, useState } from 'react';

export default function SidebarComponent() {
  const [menuOpened, setMenuOpened] = useState<boolean>(true);

  const menuClassName = useMemo<string>(() => {
    let result = 'menu';
    if (menuOpened) {
      result += ' opened';
    } else {
      result += ' closed';
    }

    return result;
  }, [menuOpened]);

  function openMenu() {
    setMenuOpened(true);
  }

  function closeMenu() {
    setMenuOpened(false);
  }

  return (
    <div className="sidebar">
      <div className="placeholder" />
      {menuOpened ? (
        <img
          src={close}
          className="trigger-button"
          alt="Close menu"
          onClick={() => closeMenu()}
        />
      ) : null}
      {!menuOpened ? (
        <img
          src={open}
          className="trigger-button"
          alt="Open menu"
          onClick={() => openMenu()}
        />
      ) : null}
      <ul className={menuClassName}>
        <li>Home</li>
        <li>Projects</li>
      </ul>
    </div>
  );
}
