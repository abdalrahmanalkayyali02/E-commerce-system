import React, { useEffect, useRef } from 'react';
import './AuthLayout.css';

interface AuthLayoutProps {
    children: React.ReactNode;
    illustrationTitle: string;
    illustrationSubtitle: string;
}

const FEATURES = [
    { icon: '🛍️', label: 'Millions of Products' },
    { icon: '🚀', label: 'Lightning-Fast Delivery' },
    { icon: '🔒', label: 'Secure Payments' },
    { icon: '⭐', label: 'Top-Rated Sellers' },
];

const AuthLayout: React.FC<AuthLayoutProps> = ({
    children,
    illustrationTitle,
    illustrationSubtitle,
}) => {
    const canvasRef = useRef<HTMLCanvasElement>(null);

    useEffect(() => {
        const canvas = canvasRef.current;
        if (!canvas) return;
        const ctx = canvas.getContext('2d');
        if (!ctx) return;

        let width = (canvas.width = canvas.offsetWidth);
        let height = (canvas.height = canvas.offsetHeight);

        const particles: {
            x: number; y: number; r: number;
            dx: number; dy: number; alpha: number;
            color: string;
        }[] = [];

        const colors = ['#6C63FF', '#FF6584', '#43E6C5', '#8B85FF'];

        for (let i = 0; i < 60; i++) {
            particles.push({
                x: Math.random() * width,
                y: Math.random() * height,
                r: Math.random() * 2.5 + 0.5,
                dx: (Math.random() - 0.5) * 0.4,
                dy: (Math.random() - 0.5) * 0.4,
                alpha: Math.random() * 0.6 + 0.2,
                color: colors[Math.floor(Math.random() * colors.length)],
            });
        }

        let animId: number;
        const draw = () => {
            ctx.clearRect(0, 0, width, height);
            particles.forEach((p) => {
                ctx.beginPath();
                ctx.arc(p.x, p.y, p.r, 0, Math.PI * 2);
                ctx.fillStyle =
                    p.color +
                    Math.round(p.alpha * 255)
                        .toString(16)
                        .padStart(2, '0');
                ctx.fill();
                p.x += p.dx;
                p.y += p.dy;
                if (p.x < 0 || p.x > width) p.dx *= -1;
                if (p.y < 0 || p.y > height) p.dy *= -1;
            });
            animId = requestAnimationFrame(draw);
        };
        draw();

        const onResize = () => {
            width = canvas.width = canvas.offsetWidth;
            height = canvas.height = canvas.offsetHeight;
        };
        window.addEventListener('resize', onResize);

        return () => {
            cancelAnimationFrame(animId);
            window.removeEventListener('resize', onResize);
        };
    }, []);

    return (
        <div className="auth-layout">
            {/* ── Left brand panel ── */}
            <div className="auth-brand">
                <canvas ref={canvasRef} className="auth-brand__canvas" />
                <div className="auth-brand__content">
                    {/* Logo */}
                    <div className="auth-brand__logo">
                        <span className="auth-brand__logo-icon">⚡</span>
                        <span className="auth-brand__logo-text">NexCart</span>
                    </div>

                    {/* Floating product card */}
                    <div className="auth-brand__card">
                        <div className="auth-brand__badge">🔥 Best Seller</div>
                        <div className="auth-brand__product">
                            <div className="auth-brand__product-img">👟</div>
                            <div className="auth-brand__product-info">
                                <p className="auth-brand__product-name">Air Boost Pro</p>
                                <p className="auth-brand__product-price">$149.99</p>
                                <div className="auth-brand__stars">★★★★★</div>
                            </div>
                        </div>
                        <div className="auth-brand__progress-label">
                            <span>Only 3 left in stock!</span>
                            <span>94%</span>
                        </div>
                        <div className="auth-brand__progress-bar">
                            <div className="auth-brand__progress-fill" style={{ width: '94%' }} />
                        </div>
                    </div>

                    {/* Headline */}
                    <h1 className="auth-brand__title">{illustrationTitle}</h1>
                    <p className="auth-brand__subtitle">{illustrationSubtitle}</p>

                    {/* Feature pills */}
                    <div className="auth-brand__features">
                        {FEATURES.map((f) => (
                            <div key={f.label} className="auth-brand__feature-pill">
                                <span>{f.icon}</span>
                                <span>{f.label}</span>
                            </div>
                        ))}
                    </div>
                </div>
            </div>

            {/* ── Right form panel ── */}
            <div className="auth-form-panel">
                <div className="auth-form-panel__inner">{children}</div>
            </div>
        </div>
    );
};

export default AuthLayout;
