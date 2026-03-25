import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { CheckCircle2, ArrowRight } from 'lucide-react';
import './SuccessPage.css';

interface SuccessPageProps {
    title: string;
    subtitle: string;
    ctaLabel: string;
    ctaHref: string;
}

const SuccessPage: React.FC<SuccessPageProps> = ({ title, subtitle, ctaLabel, ctaHref }) => {
    const [visible, setVisible] = useState(false);

    useEffect(() => {
        const t = setTimeout(() => setVisible(true), 80);
        return () => clearTimeout(t);
    }, []);

    return (
        <div className={`success-page ${visible ? 'success-page--visible' : ''}`}>
            {/* Animated checkmark */}
            <div className="success-page__icon-wrap">
                <div className="success-page__icon">
                    <CheckCircle2 size={48} strokeWidth={1.5} />
                </div>
                <div className="success-page__burst" />
                <div className="success-page__burst success-page__burst--2" />
            </div>

            {/* Content */}
            <div className="success-page__content">
                <h2 className="success-page__title">{title}</h2>
                <p className="success-page__subtitle">{subtitle}</p>
            </div>

            {/* CTA */}
            <Link to={ctaHref} className="success-page__cta" id="success-cta">
                {ctaLabel}
                <ArrowRight size={18} />
            </Link>

            {/* Confetti dots */}
            <div className="success-page__confetti" aria-hidden>
                {Array.from({ length: 8 }, (_, i) => (
                    <span key={i} className={`confetti-dot confetti-dot--${i + 1}`} />
                ))}
            </div>
        </div>
    );
};

export default SuccessPage;
