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
		5. card_token (unique)
		6. balance
		7. currency
		
		
	3. transaction:
		1. id (unique)
		2. date_time
		3. from_card
		4. to_card
		5. from_user
		6. to_user
		7. amount
		8. currency
		
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
		1. add/{token}/{name}/{currency}
		2. remove/{token}/{card_token}
		3. cards/{token}
		4. rename/{token}/{card_token}/{new_name}
		5. set_default/{token}/{card_token}
		6. get_card_data/{token}/{card_token}
		
	3. transaction_controller:
		1. send_by_card/{token}/{from_card}/{to_card}/{amount}/
		2. send_by_username/{token}/{from_username}/{to_username}/{amount}/{currency}
		3. transactions/{token}
		
	4. donation_controller:
		1. create_donation/{token}/{receiver_card_id}/{title}/{description}
		2. donations/{token}
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
		
		
		
		
		
		
		