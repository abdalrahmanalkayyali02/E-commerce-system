import type { ReactNode } from 'react';
import type { IIconButtonProps } from '../../types/interface/icon-button-props.interface';

export function IconButton({
  id,
  ariaLabel,
  onClick,
  children,
  className = '',
  badge,
}: IIconButtonProps): ReactNode {
  return (
    <button
      id={id}
      className={`navbar__icon-btn ${className}`}
      aria-label={ariaLabel}
      onClick={onClick}
    >
      {children}
      {badge !== undefined && (
        <span className="navbar__cart-badge">{badge}</span>
      )}
    </button>
  );
}
