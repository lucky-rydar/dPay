### Functionality:

1. DB:
	1. user:
		1. id (unique)
		2. email
		3. phone
		4. username (unique)
		5. password
		6. token
		
	2. card:
		1. id (unique)
		2. owner_id
		3. is_default
		4. name
		5. card_number (unique)
		6. cvv
		7. exp_month
		8. exp_year
		
	3. transaction:
		1. id (unique)
		2. date_time
		3. from_card
		4. to_card
		5. from_user
		6. to_user
		7. amount
		8. currency
		
	// entity that receive donates
	4. donation: 
		1. id (unique)
		2. donation_token (unique) (8 or 16 char)
		3. owner_id
		4. receiver_card_id
		5. title
		6. description

2. API:
	1. user_controller:
		1. register/{username}/{email}/{phone}/{password}
		2. login/{username}/{password}
		3. change_phone/{token}/{new_phone}
		
	2. card_controller:
		1. add/{token}/{number}/{month_exp}/{year_exp}/{cvv}
		2. remove/{token}/{card_id}
		3. cards/{token}
		4. rename/{token}/{card_id}/{new_name}
		5. set_default/{token}/{card_id}
		6. get_card_data/{token}/{card_id}
		
	3. transaction_controller:
		1. send_by_card/{token}/{from_card}/{to_card}/{amount}/{currency}
		2. send_by_username/{token}/{from_username}/{to_username}/{amount}/{currency}
		3. transactions/{token}
		
	4. donation_controller:
		1. create_donation/{token}/{receiver_card_id}/{title}/{description}
		2. users_donations/{token}
		3. donation_by_token/{donation_token}
		4. donate/{donation_token}/{from_card}/{amount}
		
3. Frontend
	1. login/register menu
	2. main_menu:
		1. settings
		2. profile
		3. list_of_cards
		4. send_money
		5. donations
			1. create_donate
			2. find
			2. donate
		
		
		
		
		
		
		