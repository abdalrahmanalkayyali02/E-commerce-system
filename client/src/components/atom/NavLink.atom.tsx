import type { ReactNode } from 'react';
import type { INavLinkProps } from '../../types/interface/nav-link-props.interface';

export function NavLink({ href, label, onClick, className = '' }: INavLinkProps): ReactNode {
  return (
    <a href={href} className={`navbar__link ${className}`} onClick={onClick}>
      {label}
      <span className="navbar__link-underline" />
    </a>
  );
}
