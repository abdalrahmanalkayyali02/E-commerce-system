import type { ReactNode } from 'react';
import type { ILogoProps } from '../../types/interface/logo-props.interface';

export function Logo({ name = 'VORA', className = '' }: ILogoProps): ReactNode {
  return (
    <a href="#" className={`logo ${className}`}>
      <span className="logo__icon">◆</span>
      <span className="logo__text">{name}</span>
    </a>
  );
}
