import { User } from "@models/user/user";

/**
 * Represent sign in response data
 */
export interface SignInResponse {
  /**
   * Authenticated user
   */
  user: User;
}
