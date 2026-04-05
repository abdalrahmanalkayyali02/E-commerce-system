import type { ReactNode } from 'react';
import { useState, useEffect } from 'react';
import { motion, AnimatePresence } from 'framer-motion';
import { ShoppingBag, Search, Menu, X, User, Heart } from 'lucide-react';
import { Logo } from '../../atom/Logo.atom';
import { NavLink } from '../../atom/NavLink.atom';
import { IconButton } from '../../atom/Button.icon';
import { navLinks } from '../metadata/NavBar.metadata';
import type { INavLink } from '../../../types/interface/nav-link.interface';

export function Navbar(): ReactNode {
  const [scrolled, setScrolled] = useState<boolean>(false);
  const [mobileOpen, setMobileOpen] = useState<boolean>(false);
  const [searchOpen, setSearchOpen] = useState<boolean>(false);

  useEffect((): (() => void) => {
    const handler = (): void => setScrolled(window.scrollY > 40);
    window.addEventListener('scroll', handler, { passive: true });
    return (): void => window.removeEventListener('scroll', handler);
  }, []);

  return (
    <>
      <motion.header
        id="navbar"
        className={`navbar ${scrolled ? 'navbar--scrolled' : ''}`}
        initial={{ y: -80 }}
        animate={{ y: 0 }}
        transition={{ duration: 0.6, ease: [0.22, 1, 0.36, 1] }}
      >
        <div className="navbar__inner">
          <Logo name="VORA" className="navbar__logo" />

          <nav className="navbar__nav" id="main-nav">
            {navLinks.map((link: INavLink) => (
              <NavLink key={link.label} href={link.href} label={link.label} />
            ))}
          </nav>

          <div className="navbar__actions">
            <IconButton
              id="search-toggle"
              ariaLabel="Search"
              onClick={(): void => setSearchOpen(!searchOpen)}
            >
              <Search size={20} />
            </IconButton>
            <IconButton id="wishlist-btn" ariaLabel="Wishlist">
              <Heart size={20} />
            </IconButton>
            <IconButton id="account-btn" ariaLabel="Account">
              <User size={20} />
            </IconButton>
            <IconButton
              id="cart-btn"
              ariaLabel="Cart"
              className="navbar__cart-btn"
              badge={3}
            >
              <ShoppingBag size={20} />
            </IconButton>
            <IconButton
              id="mobile-menu-toggle"
              ariaLabel="Menu"
              className="navbar__menu-btn"
              onClick={(): void => setMobileOpen(!mobileOpen)}
            >
              {mobileOpen ? <X size={22} /> : <Menu size={22} />}
            </IconButton>
          </div>
        </div>

        <AnimatePresence>
          {searchOpen && (
            <motion.div
              className="navbar__search"
              initial={{ height: 0, opacity: 0 }}
              animate={{ height: 'auto', opacity: 1 }}
              exit={{ height: 0, opacity: 0 }}
              transition={{ duration: 0.3 }}
            >
              <div className="navbar__search-inner">
                <Search size={18} className="navbar__search-icon" />
                <input
                  type="text"
                  placeholder="Search for products, brands, and more..."
                  className="navbar__search-input"
                  id="search-input"
                  autoFocus
                />
              </div>
            </motion.div>
          )}
        </AnimatePresence>
      </motion.header>

      <AnimatePresence>
        {mobileOpen && (
          <motion.div
            className="mobile-menu"
            id="mobile-menu"
            initial={{ opacity: 0, x: '100%' }}
            animate={{ opacity: 1, x: 0 }}
            exit={{ opacity: 0, x: '100%' }}
            transition={{ duration: 0.4, ease: [0.22, 1, 0.36, 1] }}
          >
            <nav className="mobile-menu__nav">
              {navLinks.map((link: INavLink, index: number) => (
                <motion.a
                  key={link.label}
                  href={link.href}
                  className="mobile-menu__link"
                  initial={{ opacity: 0, x: 40 }}
                  animate={{ opacity: 1, x: 0 }}
                  transition={{ delay: 0.1 + index * 0.06 }}
                  onClick={(): void => setMobileOpen(false)}
                >
                  {link.label}
                </motion.a>
              ))}
            </nav>
          </motion.div>
        )}
      </AnimatePresence>
    </>
  );
}
