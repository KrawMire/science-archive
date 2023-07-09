import { NgModule } from "@angular/core";
import { ErrorBannerComponent } from "./components/error-banner/error-banner.component";
import { CommonModule } from "@angular/common";
import { ReadMoreButtonComponent } from "./components/read-more-button/read-more-button.component";

@NgModule({
  imports: [CommonModule],
  declarations: [ErrorBannerComponent, ReadMoreButtonComponent],
  exports: [ErrorBannerComponent, CommonModule, ReadMoreButtonComponent],
})
export default class SharedModule {}
