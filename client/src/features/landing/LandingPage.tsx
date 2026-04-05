import { Navbar } from '../../components/Molecules/ui/NavBar.ui';
import { HeroSection } from './HeroSection';
import { CategoryShowcase } from '../../components/Molecules/ui/CategoryShowcase.ui';
import { FeaturedProducts } from '../../components/Molecules/ui/FeaturedProducts.ui';
import { FeaturesBar } from '../../components/Molecules/ui/FeaturesBar.ui';
import { Newsletter } from '../../components/Molecules/ui/Newsletter.ui';
import { Footer } from '../../components/Molecules/ui/Footer.ui';
import './landing.css';

export function LandingPage() {
  return (
    <div className="landing" id="landing-page">
      <Navbar />
      <HeroSection />
      <CategoryShowcase />
      <FeaturedProducts />
      <FeaturesBar />
      <Newsletter />
      <Footer />
    </div>
  );
}
