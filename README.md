DB:
	user:
		id (unique)
		email
		phone
		username (unique)
		password
		token
		
	card:
		id (unique)
		owner_id
		is_default
		name
		card_number (unique)
		cvv
		exp_month
		exp_year
		
	transaction:
		id (unique)
		date_time
		from_card
		to_card
		from_user
		to_user
		amount
		

API:
	user_controller:
		register/{phone}/{email}/{username}/{password}
		login/{username}/{password}
		
	card_controller:
		add/{token}/{number}/{month_exp}/{year_exp}/{cvv}
		remove/{token}/{number}
		cards/{token}
		rename/{token}/{card_number}/{name}
		set_default/{card_id}
		get_cvv/{card_id}
		get_date/{card_id}
		
	transaction_controller:
		send/by_card_num/{token}/{from_card_id}/{to_card_num}/{amount}
		send/by_card_id/{token}/{from_card_id}/{to_card_id}/{amount}
		
		