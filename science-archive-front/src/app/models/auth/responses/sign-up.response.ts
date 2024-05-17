import { User } from "@models/user/user";

/**
 * Represent data given from server to sign up request
 */
export interface SignUpResponse {
  /**
   * New created user
   */
  user: User;
}
